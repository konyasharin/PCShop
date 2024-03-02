import { createSlice } from '@reduxjs/toolkit';
import TCheckBox from 'types/TCheckBox.ts';
import EComponentTypes from 'enums/EComponentTypes.ts';

export type TFilters = {
  checkBoxesBlocks: {
    name: string;
    checkBoxes: TCheckBox[];
  }[];
  radiosBlocks: {
    name: string;
    checkBoxes: TCheckBox[];
  }[];
  inputsBlocks: {
    name: string;
    inputs: { value: string; name: string; placeholder: string }[];
  }[];
};

type TFiltersState = {
  [componentType in EComponentTypes]: TFilters;
};

const initialState: TFiltersState = {
  videoCard: {
    checkBoxesBlocks: [
      {
        name: 'Производитель',
        checkBoxes: [
          { text: 'Gigabyte' },
          { text: 'Asrock' },
          { text: 'ASUS' },
        ],
      },
      {
        name: 'Видеопамять',
        checkBoxes: [{ text: '4GB' }, { text: '8GB' }, { text: '16GB' }],
      },
    ],
    radiosBlocks: [
      {
        name: 'Тест',
        checkBoxes: [{ text: 'Тест' }, { text: 'Тест1' }],
      },
    ],
    inputsBlocks: [
      {
        name: 'Цена',
        inputs: [
          { value: '', name: 'PriceFrom', placeholder: 'От' },
          { value: '', name: 'PriceTo', placeholder: 'До' },
        ],
      },
    ],
  },
  processor: {
    checkBoxesBlocks: [
      {
        name: 'Производитель',
        checkBoxes: [{ text: 'Intel' }, { text: 'AMD' }],
      },
      {
        name: 'Число ядер',
        checkBoxes: [{ text: '4' }, { text: '8' }, { text: '16' }],
      },
    ],
    radiosBlocks: [
      {
        name: 'Тест',
        checkBoxes: [{ text: 'Тест' }, { text: 'Тест1' }],
      },
    ],
    inputsBlocks: [
      {
        name: 'Цена',
        inputs: [
          { value: '', name: 'PriceFrom', placeholder: 'От' },
          { value: '', name: 'PriceTo', placeholder: 'До' },
        ],
      },
    ],
  },
};

const filtersSlice = createSlice({
  name: 'filters',
  initialState,
  reducers: {},
});

export default filtersSlice.reducer;