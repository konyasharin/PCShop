import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import TCheckBox from 'types/TCheckBox.ts';
import TVideoCardFilters from 'types/components/TVideoCardFilters.ts';
import TProcessorFilters from 'types/components/TProcessorFilters.ts';
import componentTypes from 'enums/componentTypes.ts';
import TMotherBoardFilters from 'types/components/TMotherBoardFilters.ts';

export type TComponentFilterKeys<T> = {
  [key in keyof T]: TCheckBox[];
};

export type TFilters = {
  videoCard: TComponentFilterKeys<TVideoCardFilters>;
  processor: TComponentFilterKeys<TProcessorFilters>;
  motherBoard: TComponentFilterKeys<TMotherBoardFilters>;
};

const initialState: TFilters = {
  videoCard: {
    brand: [],
    model: [],
    memoryType: [],
    memoryDb: [],
  },
  processor: {
    brand: [],
    model: [],
    cores: [],
    clockFrequency: [],
    turboFrequency: [],
    heatDissipation: [],
  },
  motherBoard: {
    brand: [],
    model: [],
    frequency: [],
    socket: [],
    chipset: [],
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
        state[action.payload.componentType][key] = newFilters;
      }
    },
  },
});

export default filtersSlice.reducer;
export const { setFilters } = filtersSlice.actions;
