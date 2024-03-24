import { useRef, useState } from 'react';

function useBorderValues(
  initialValue: number,
  from?: number,
  to?: number,
): [number, (newValue: number) => void] {
  const [value, setValue] = useState(initialValue);
  const fromValue = useRef(from);
  const toValue = useRef(to);

  function handleSetValue(newValue: number) {
    if (
      (fromValue.current || fromValue.current === 0) &&
      newValue < fromValue.current
    ) {
      setValue(fromValue.current);
    } else if (
      (toValue.current || toValue.current === 0) &&
      newValue > toValue.current
    ) {
      setValue(toValue.current);
    } else {
      setValue(newValue);
    }
  }
  return [value, handleSetValue];
}

export default useBorderValues;
