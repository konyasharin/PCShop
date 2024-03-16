import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import TUser from 'types/auth/TUser.ts';

type TUserInfo = { isAuth: boolean; userInfo?: TUser };
const initialState: TUserInfo = {
  isAuth: false,
};
const profileSlice = createSlice({
  name: 'profile',
  initialState,
  reducers: {
    setIsAuth: (state, action: PayloadAction<boolean>) => {
      state.isAuth = action.payload;
    },
    setUserData: (state, action: PayloadAction<{ data: TUser }>) => {
      state.userInfo = action.payload.data;
    },
  },
});

export default profileSlice.reducer;

export const { setIsAuth, setUserData } = profileSlice.actions;
