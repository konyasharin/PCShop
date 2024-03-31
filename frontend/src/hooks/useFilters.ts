import { useEffect, useRef, useState } from 'react';
import componentTypes from 'enums/componentTypes.ts';
import TComponentFiltersKeys from 'types/components/TComponentFiltersKeys.ts';
import { useSelector } from 'react-redux';
import { RootState } from 'store/store.ts';

function useFilters(
  initialType: 'radio' | 'checkBox',
  initialComponentType: keyof typeof componentTypes,
) {
  const type = useRef(initialType);
  const [componentType, setComponentType] = useState(initialComponentType);
  const filtersState = useSelector(
    (state: RootState) => state.filters[componentType],
  );
  const [filters, setFilters] = useState<TComponentFiltersKeys>(
    initFilters(filtersState),
  );
  useEffect(() => {
    setFilters(initFilters(filtersState));
  }, [filtersState]);

  function initFilters(initFilters: TComponentFiltersKeys) {
    let k: keyof TComponentFiltersKeys;
    const newFilters = JSON.parse(
      JSON.stringify(initFilters),
    ) as TComponentFiltersKeys; // Создание глубокой копии
    for (k in initFilters) {
      newFilters[k] = newFilters[k].map(filter => {
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
      [nameBlock]: filters[nameBlock].map((filter, i) => {
        if (i === index && type.current === 'checkBox') {
          filter.isActive = newIsActive;
        }
        return filter;
      }),
    };
    setFilters(newFilters);
  }

  function setRadioIsActive(
    nameBlock: keyof TComponentFiltersKeys,
    index: number,
  ) {
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

  function findIndexOfFilter(
    nameBlock: keyof TComponentFiltersKeys,
    filterText: string,
  ): null | number {
    filters[nameBlock].map((filter, i) => {
      if (filter.text === filterText) {
        return i;
      }
    });
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
