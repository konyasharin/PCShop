import { createSlice } from '@reduxjs/toolkit';
import TCheckBox from 'types/TCheckBox.ts';
import TVideoCardFilters from 'types/components/TVideoCardFilters.ts';
import TProcessorFilters from 'types/components/TProcessorFilters.ts';

export type TFilter = {
  text: string;
  filters: TCheckBox[];
};

export type TComponentFilterKeys<T> = {
  [key in keyof T]: TFilter;
};

export type TFilters = {
  videoCard: TComponentFilterKeys<TVideoCardFilters>;
  processor: TComponentFilterKeys<TProcessorFilters>;
};

const initialState: TFilters = {
  videoCard: {
    brand: {
      text: 'Производитель',
      filters: [{ text: 'Asrock' }, { text: 'ASUS' }, { text: 'Gigabyte' }],
    },
    model: {
      text: 'Модель',
      filters: [
        { text: 'RTX 3090' },
        { text: 'RTX 3080' },
        { text: 'RTX 4060' },
      ],
    },
    memoryType: {
      text: 'Тип видеопамяти',
      filters: [{ text: 'DDR4' }, { text: 'DDR5' }],
    },
    memoryDb: {
      text: 'Количество видеопамяти',
      filters: [{ text: '1GB' }, { text: '2GB' }],
    },
  },
  processor: {
    brand: {
      text: 'Производитель',
      filters: [{ text: 'Intel' }, { text: 'AMD' }],
    },
    model: {
      text: 'Модель',
      filters: [{ text: 'Ryzen 5 2600' }, { text: 'Ryzen 5 3600X' }],
    },
    cores: {
      text: 'Ядра',
      filters: [{ text: '1' }, { text: '2' }],
    },
    clockFrequency: {
      text: 'Частота',
      filters: [{ text: '0-2.7 ГГц' }, { text: '2.7-3.1 ГГц' }],
    },
    turboFrequency: {
      text: 'Частота в турбо режиме',
      filters: [{ text: '0-2.7 ГГц' }, { text: '2.7-3.1 ГГц' }],
    },
    heatDissipation: {
      text: 'Рассеиваемая мощность',
      filters: [{ text: '0-50 Вт' }, { text: '50-70 Вт' }],
    },
  },
};

const filtersSlice = createSlice({
  name: 'filters',
  initialState,
  reducers: {},
});

export default filtersSlice.reducer;
