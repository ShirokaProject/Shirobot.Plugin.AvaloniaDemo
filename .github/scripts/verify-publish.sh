#!/usr/bin/env bash
set -euo pipefail

publish_dir="${1:?publish directory is required}"
expected_dll="${2:?expected plugin DLL is required}"

if [[ ! -d "$publish_dir" ]]; then
  echo "Publish directory not found: $publish_dir" >&2
  exit 1
fi

shopt -s globstar nullglob
dlls=("$publish_dir"/**/*.dll)

if (( ${#dlls[@]} != 1 )); then
  echo "Expected exactly one DLL in $publish_dir, found ${#dlls[@]}:" >&2
  printf '  %s\n' "${dlls[@]}" >&2
  exit 1
fi

if [[ "${dlls[0]}" != "$publish_dir/$expected_dll" ]]; then
  echo "Unexpected published DLL: ${dlls[0]}" >&2
  exit 1
fi

runtime_metadata=("$publish_dir"/**/*.deps.json "$publish_dir"/**/*.runtimeconfig.json)
if (( ${#runtime_metadata[@]} != 0 )); then
  echo "Plugin package contains host/runtime metadata:" >&2
  printf '  %s\n' "${runtime_metadata[@]}" >&2
  exit 1
fi

runtime_files=()
if [[ -d "$publish_dir/runtimes" ]]; then
  for runtime_file in "$publish_dir"/runtimes/**/*; do
    [[ -f "$runtime_file" ]] && runtime_files+=("$runtime_file")
  done
fi

if (( ${#runtime_files[@]} != 0 )); then
  echo "Plugin package contains shared runtime files:" >&2
  printf '  %s\n' "${runtime_files[@]}" >&2
  exit 1
fi

native_runtime=("$publish_dir"/**/*.so "$publish_dir"/**/*.dylib)
if (( ${#native_runtime[@]} != 0 )); then
  echo "Plugin package contains shared native runtime files:" >&2
  printf '  %s\n' "${native_runtime[@]}" >&2
  exit 1
fi

echo "Verified single-DLL plugin package: $publish_dir/$expected_dll"
