import { useRef, useState } from 'react';

/**
 * Хук для ограничения хранения чисел в useState в определенном диапазоне
 * @param initialValue начальное значение числа
 * @param from минимально возможное значение (если не установлено, то минимального значения не будет)
 * @param to максимально возможное значение (если не установлено, то максимального значения не будет)
 */
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
