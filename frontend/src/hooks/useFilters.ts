import { useRef, useState } from 'react';
import { TFilters } from 'store/slices/filtersSlice.ts';
import componentTypes from 'enums/componentTypes.ts';
import TComponentFiltersKeys from 'types/components/TComponentFiltersKeys.ts';

function useFilters(
  filtersState: TFilters,
  initialType: 'radio' | 'checkBox',
  initialComponentType: keyof typeof componentTypes,
) {
  const type = useRef(initialType);
  const componentType = useRef(initialComponentType);
  const [filters, setFilters] = useState<TComponentFiltersKeys>(
    initFilters(filtersState[componentType.current]),
  );

  function setComponentTypeHandle(
    newComponentType: keyof typeof componentTypes,
  ) {
    componentType.current = newComponentType;
    setFilters(initFilters(filtersState[componentType.current]));
  }

  function initFilters(initFilters: TComponentFiltersKeys) {
    let k: keyof TComponentFiltersKeys;
    const newFilters = JSON.parse(
      JSON.stringify(initFilters),
    ) as TComponentFiltersKeys; // Создание глубокой копии
    for (k in initFilters) {
      newFilters[k].filters = newFilters[k].filters.map(filter => {
        return {
          ...filter,
          isActive: false,
        };
      });
    }
    return newFilters;
  }

  function setCheckBoxIsActive(
    nameBlock: keyof TComponentFiltersKeys,
    index: number,
    newIsActive: boolean,
  ) {
    const newFilters = {
      ...filters,
      [nameBlock]: {
        ...filters[nameBlock],
        filters: filters[nameBlock].filters.map((filter, i) => {
          if (i === index && type.current === 'checkBox') {
            filter.isActive = newIsActive;
          }
          return filter;
        }),
      },
    };
    setFilters(newFilters);
  }

  function setRadioIsActive(
    nameBlock: keyof TComponentFiltersKeys,
    index: number,
  ) {
    const newFilters = {
      ...filters,
      [nameBlock]: {
        ...filters[nameBlock],
        filters: filters[nameBlock].filters.map((filter, i) => {
          if (type.current === 'radio') {
            filter.isActive = index === i;
          }
          return filter;
        }),
      },
    };
    setFilters(newFilters);
  }

  return {
    filters,
    setCheckBoxIsActive,
    setRadioIsActive,
    setComponentTypeHandle,
  };
}

export default useFilters;
