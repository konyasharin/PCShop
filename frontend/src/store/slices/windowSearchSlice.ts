import { createSlice, PayloadAction } from '@reduxjs/toolkit';

export type TSearchBlock = {
  img: string;
  title: string;
  text: string;
  isActive: boolean;
};

type TWindowSearchState = {
  searchWindowIsActive: boolean;
  blocks: TSearchBlock[];
  searchBtnIsActive: boolean;
};

const initialState: TWindowSearchState = {
  searchWindowIsActive: false,
  blocks: [],
  searchBtnIsActive: true,
};

export const windowSearchSlice = createSlice({
  name: 'windowSearch',
  initialState,
  reducers: {
    setActivateBlock: (
      state,
      action: PayloadAction<{ index: number; newIsActive: boolean }>,
    ) => {
      state.blocks[action.payload.index].isActive = action.payload.newIsActive;
    },
    setBlocks: (state, action: PayloadAction<TSearchBlock[]>) => {
      state.blocks = action.payload;
    },
    setSearchWindowIsActive: (state, action: PayloadAction<boolean>) => {
      state.searchWindowIsActive = action.payload;
    },
    setSearchBtnIsActive: (state, action: PayloadAction<boolean>) => {
      state.searchBtnIsActive = action.payload;
    },
  },
});

export default windowSearchSlice.reducer;
export const {
  setActivateBlock,
  setBlocks,
  setSearchWindowIsActive,
  setSearchBtnIsActive,
} = windowSearchSlice.actions;
