import { useEffect, useRef, useState } from 'react';
import componentTypes from 'enums/componentTypes.ts';
import { TComponentFilterKeys, TFilters } from 'hooks/useAllFilters.ts';

function useFilters(
  initialType: 'radio' | 'checkBox',
  initialComponentType: keyof typeof componentTypes,
  allFilters: TFilters | null,
) {
  const type = useRef(initialType);
  const [componentType, setComponentType] = useState(initialComponentType);
  const [filters, setFilters] = useState<TComponentFilterKeys | null>(
    initFilters(allFilters),
  );
  useEffect(() => {
    setFilters(initFilters(allFilters));
  }, [allFilters, componentType]);

  function initFilters(initFilters: TFilters | null) {
    if (initFilters != null) {
      let k: keyof TComponentFilterKeys;
      const newFilters = JSON.parse(
        JSON.stringify(initFilters[componentType]),
      ) as TComponentFilterKeys; // Создание глубокой копии
      for (k in newFilters) {
        newFilters[k] = newFilters[k].map(filter => {
          return {
            ...filter,
            isActive: false,
          };
        });
      }
      return newFilters;
    }
    return null;
  }

  function setCheckBoxIsActive(
    nameBlock: keyof TComponentFilterKeys,
    index: number,
    newIsActive: boolean,
  ) {
    if (filters != null) {
      const newFilters = {
        ...filters,
        [nameBlock]: filters[nameBlock].map((filter, i) => {
          if (i === index && type.current === 'checkBox') {
            filter.isActive = newIsActive;
          }
          return filter;
        }),
      };
      setFilters(newFilters);
    }
  }

  function setRadioIsActive(
    nameBlock: keyof TComponentFilterKeys,
    index: number,
  ) {
    if (filters != null) {
      const newFilters = {
        ...filters,
        [nameBlock]: filters[nameBlock].map((filter, i) => {
          if (type.current === 'radio') {
            filter.isActive = index === i;
          }
          return filter;
        }),
      };
      setFilters(newFilters);
    }
  }

  function findIndexOfFilter(
    nameBlock: keyof TComponentFilterKeys,
    filterText: string,
  ): null | number {
    if (filters != null) {
      filters[nameBlock].forEach((filter, i) => {
        if (filter.text === filterText) {
          return i;
        }
      });
    }
    return null;
  }

  return {
    filters,
    setCheckBoxIsActive,
    setRadioIsActive,
    findIndexOfFilter,
    setComponentType,
  };
}

export default useFilters;
