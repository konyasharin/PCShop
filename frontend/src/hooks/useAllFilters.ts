import TCheckBox from 'types/TCheckBox.ts';
import TOneOfComponentsFilters from 'types/components/TOneOfComponentsFilters.ts';
import componentTypes from 'enums/componentTypes.ts';
import { useEffect, useState } from 'react';
import getFilters from 'api/filters/getFilters.ts';
import { useDispatch } from 'react-redux';
import { setIsLoading } from 'store/slices/loadingSlice.ts';

export type TComponentFilterKeys = {
  [key in keyof TOneOfComponentsFilters]: TCheckBox[];
};

export type TFilters = {
  [key in keyof typeof componentTypes]: TComponentFilterKeys;
};

/**
 * Хук для хранения всех фильтров для всех компонентов
 */
function useAllFilters() {
  const [allFilters, setAllFilters] = useState<TFilters | null>(null);
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(setIsLoading(true));
    getAllFilters().then(newAllFilters => {
      setAllFilters(newAllFilters);
      dispatch(setIsLoading(false));
    });
  }, []);
  async function getAllFilters() {
    const newAllFilters: {
      [key: string]: { [key: string]: { text: string }[] };
    } = {};
    let key: keyof typeof componentTypes;
    for (key in componentTypes) {
      newAllFilters[key] = {};
      const gotFilters = (await getFilters(key)).data;
      let keyFilters: keyof typeof gotFilters;
      for (keyFilters in gotFilters) {
        newAllFilters[key][keyFilters] = [];
        gotFilters[keyFilters].forEach(filterText => {
          newAllFilters[key][keyFilters].push({ text: filterText });
        });
      }
    }
    return newAllFilters as TFilters;
  }
  return { allFilters };
}

export default useAllFilters;
