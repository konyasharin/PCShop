import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import EComponentTypes from 'enums/EComponentTypes.ts';

type TChooseComponents = {
  [componentType in EComponentTypes]: {
    isActive: boolean;
  };
};

const initialState: TChooseComponents = {
  videoCard: {
    isActive: false,
  },
  processor: {
    isActive: false,
  },
};

const chooseComponentsSlice = createSlice({
  name: 'chooseComponents',
  initialState,
  reducers: {
    setIsActive: (
      state,
      action: PayloadAction<{ type: EComponentTypes; newIsActive: boolean }>,
    ) => {
      state[action.payload.type].isActive = action.payload.newIsActive;
    },
  },
});

export default chooseComponentsSlice.reducer;
export const { setIsActive } = chooseComponentsSlice.actions;
