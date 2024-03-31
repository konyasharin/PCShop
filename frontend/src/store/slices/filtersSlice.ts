import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import TCheckBox from 'types/TCheckBox.ts';
import TVideoCardFilters from 'types/components/TVideoCardFilters.ts';
import TProcessorFilters from 'types/components/TProcessorFilters.ts';
import componentTypes from 'enums/componentTypes.ts';

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
      filters: [],
    },
    model: {
      text: 'Модель',
      filters: [],
    },
    memoryType: {
      text: 'Тип видеопамяти',
      filters: [],
    },
    memoryDb: {
      text: 'Количество видеопамяти',
      filters: [],
    },
  },
  processor: {
    brand: {
      text: 'Производитель',
      filters: [],
    },
    model: {
      text: 'Модель',
      filters: [],
    },
    cores: {
      text: 'Ядра',
      filters: [],
    },
    clockFrequency: {
      text: 'Частота',
      filters: [],
    },
    turboFrequency: {
      text: 'Частота в турбо режиме',
      filters: [],
    },
    heatDissipation: {
      text: 'Рассеиваемая мощность',
      filters: [],
    },
  },
};

const filtersSlice = createSlice({
  name: 'filters',
  initialState,
  reducers: {
    setFilters: (
      state,
      action: PayloadAction<{
        componentType: keyof typeof componentTypes;
        filters: {
          [key in keyof (TVideoCardFilters | TProcessorFilters)]: string[];
        };
      }>,
    ) => {
      let key: keyof typeof action.payload.filters;
      for (key in action.payload.filters) {
        const newFilters: TCheckBox[] = [];
        action.payload.filters[key].forEach(filterText => {
          newFilters.push({ text: filterText });
        });
        state[action.payload.componentType][key].filters = newFilters;
      }
    },
  },
});

export default filtersSlice.reducer;
export const { setFilters } = filtersSlice.actions;
