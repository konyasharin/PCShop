import { createSlice } from '@reduxjs/toolkit';
import TCheckBox from 'types/TCheckBox.ts';
import TComponentTypes from 'types/TComponentTypes.ts';

export type filtersState = TComponentTypes & {
  checkBoxes: { name: string; checkBoxes: TCheckBox[] }[];
  radios: { name: string; checkBoxes: TCheckBox[] }[];
  inputs: {
    name: string;
    inputs: { from: { value: string }; to: { value: string } };
  }[];
};

const initialState: filtersState[] = [
  {
    type: 'videoCard',
    checkBoxes: [
      {
        name: 'Производитель',
        checkBoxes: [
          { text: 'Gigabyte', isActive: false },
          { text: 'Asrock', isActive: false },
          { text: 'ASUS', isActive: false },
        ],
      },
      {
        name: 'Видеопамять',
        checkBoxes: [
          { text: '4GB', isActive: false },
          { text: '8GB', isActive: false },
          { text: '16GB', isActive: false },
        ],
      },
    ],
    radios: [
      {
        name: 'Тест',
        checkBoxes: [
          { text: 'Тест', isActive: false },
          { text: 'Тест1', isActive: false },
        ],
      },
    ],
    inputs: [
      {
        name: 'Цена',
        inputs: {
          from: {
            value: '0',
          },
          to: {
            value: '100000',
          },
        },
      },
    ],
  },
  {
    type: 'processor',
    checkBoxes: [
      {
        name: 'Производитель',
        checkBoxes: [
          { text: 'Intel', isActive: false },
          { text: 'AMD', isActive: false },
        ],
      },
      {
        name: 'Ядра',
        checkBoxes: [
          { text: '4 ядра', isActive: false },
          { text: '8 ядер', isActive: false },
          { text: '12 ядер', isActive: false },
        ],
      },
    ],
    radios: [
      {
        name: 'Тест',
        checkBoxes: [
          { text: 'Тест', isActive: false },
          { text: 'Тест1', isActive: false },
        ],
      },
    ],
    inputs: [
      {
        name: 'Цена',
        inputs: {
          from: {
            value: '0',
          },
          to: {
            value: '100000',
          },
        },
      },
    ],
  },
];

const filtersSlice = createSlice({
  name: 'filters',
  initialState,
  reducers: {},
});

export default filtersSlice.reducer;
