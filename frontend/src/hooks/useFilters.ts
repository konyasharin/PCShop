import { useEffect, useRef, useState } from 'react';
import componentTypes from 'enums/componentTypes.ts';
import { TComponentFilterKeys, TFilters } from 'hooks/useAllFilters.ts';
import TCheckBox from 'types/TCheckBox.ts';

/**
 * Хук для фильтрации компонентов определенного типа
 * @param initialType начальный тип чекбокса
 * @param initialComponentType начальный тип компонента
 * @param allFilters все фильтры для компонентов всех типов
 */
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
      for (let i = 0; i < filters[nameBlock].length; i++) {
        if (filters[nameBlock][i].text === filterText) {
          return i;
        }
      }
    }
    return null;
  }

  function searchActiveFromFilter(filter: TCheckBox[]): TCheckBox | null {
    let result: null | TCheckBox = null;
    filter.forEach(filterElem => {
      if (filterElem.isActive) {
        result = filterElem;
      }
    });
    return result;
  }

  function searchActivesFilters(filters: TComponentFilterKeys) {
    return Object.keys(filters).reduce(
      (acc, key) => {
        const active = searchActiveFromFilter(
          filters[key as keyof typeof filters],
        );
        acc[key as keyof typeof filters] = active ? active.text : '';
        return acc;
      },
      {} as { [key in keyof typeof filters]: string },
    );
  }

  return {
    filters,
    setCheckBoxIsActive,
    setRadioIsActive,
    findIndexOfFilter,
    setComponentType,
    searchActiveFromFilter,
    searchActivesFilters,
  };
}

export default useFilters;
