import { createSlice } from '@reduxjs/toolkit';
import TCheckBox from 'types/TCheckBox.ts';
import EComponentTypes from 'enums/EComponentTypes.ts';

export type TFilters = {
  type: 'checkBox' | 'radio';
  name: string;
  filters: TCheckBox[];
}[];

type TFiltersState = {
  [componentType in EComponentTypes]: TFilters;
};

const initialState: TFiltersState = {
  videoCard: [
    {
      type: 'checkBox',
      name: 'Производитель',
      filters: [{ text: 'Asrock' }, { text: 'ASUS' }, { text: 'Gigabyte' }],
    },
    {
      type: 'radio',
      name: 'Тест',
      filters: [{ text: 'Тест1' }, { text: 'Тест2' }, { text: 'Тест3' }],
    },
  ],
  processor: [
    {
      type: 'checkBox',
      name: 'Производитель',
      filters: [{ text: 'Intel' }, { text: 'AMD' }],
    },
  ],
};

const filtersSlice = createSlice({
  name: 'filters',
  initialState,
  reducers: {},
});

export default filtersSlice.reducer;
