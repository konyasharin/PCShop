import { createSlice } from '@reduxjs/toolkit';
import TCheckBox from 'types/TCheckBox.ts';
import EComponentTypes from 'enums/EComponentTypes.ts';

export type TFilter = {
  name: string;
  text: string;
  filters: TCheckBox[];
};

export type TFilters = TFilter[];

type TFiltersState = {
  [componentType in EComponentTypes]: TFilters;
};

const initialState: TFiltersState = {
  videoCard: [
    {
      name: 'brand',
      text: 'Производитель',
      filters: [{ text: 'Asrock' }, { text: 'ASUS' }, { text: 'Gigabyte' }],
    },
    {
      name: 'model',
      text: 'Модель',
      filters: [
        { text: 'RTX 3090' },
        { text: 'RTX 3080' },
        { text: 'RTX 4060' },
      ],
    },
    {
      name: 'memoryType',
      text: 'Тип видеопамяти',
      filters: [{ text: 'DDR4' }, { text: 'DDR5' }],
    },
    {
      name: 'memoryDb',
      text: 'Количество видеопамяти',
      filters: [{ text: '1GB' }, { text: '2GB' }],
    },
  ],
  processor: [
    {
      name: 'brand',
      text: 'Производитель',
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
