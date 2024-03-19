import { useRef, useState } from 'react';
import { TFilter, TFilters } from 'store/slices/filtersSlice.ts';

export type TFilterType = TFilter & {
  type: 'radio' | 'checkBox';
};

export type TFiltersType = TFilterType[];

function useFilters(filtersState: TFilters, initialType: 'radio' | 'checkBox') {
  const type = useRef(initialType);
  const [filters, setFilters] = useState<TFiltersType>(
    initFilters(filtersState),
  );

  function initFilters(newFilters: TFilters): TFiltersType {
    return newFilters.map(filter => {
      return {
        ...filter,
        type: type.current,
        filters: filter.filters.map(filterElem => {
          return {
            text: filterElem.text,
            isActive: false,
          };
        }),
      };
    });
  }

  function setFiltersHandle(newFilters: TFilters) {
    setFilters(initFilters(newFilters));
  }

  function setCheckBoxIsActive(
    nameBlock: string,
    index: number,
    newIsActive: boolean,
  ) {
    setFilters(
      filters.map(filter => {
        if (nameBlock === filter.name && filter.type === 'checkBox') {
          filter.filters[index].isActive = newIsActive;
        }
        return filter;
      }),
    );
  }

  function setRadioIsActive(nameBlock: string, index: number) {
    setFilters(
      filters.map(filter => {
        if (filter.name === nameBlock && filter.type === 'radio') {
          filter.filters.forEach((filterElem, i) => {
            filterElem.isActive = index === i;
          });
        }
        return filter;
      }),
    );
  }

  return { filters, setCheckBoxIsActive, setRadioIsActive, setFiltersHandle };
}

export default useFilters;
