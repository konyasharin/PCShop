import { createSlice } from '@reduxjs/toolkit';

type TUserInfo = { isAuth: boolean };
const initialState: TUserInfo = { isAuth: false };
const profileSlice = createSlice({
  name: 'profile',
  initialState,
  reducers: {
    setIsAuth: (state, action) => {
      state.isAuth = action.payload;
    },
  },
});

export default profileSlice.reducer;

export const { setIsAuth } = profileSlice.actions;
