import { useState } from 'react';
import { TFilters } from 'store/slices/filtersSlice.ts';

function useFilters(filtersState: TFilters) {
  const [filters, setFilters] = useState<TFilters>(
    filtersState.map(filter => {
      return {
        ...filter,
        filters: filter.filters.map(filterElem => {
          return {
            text: filterElem.text,
            isActive: false,
          };
        }),
      };
    }),
  );

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

  return { filters, setCheckBoxIsActive, setRadioIsActive };
}

export default useFilters;
