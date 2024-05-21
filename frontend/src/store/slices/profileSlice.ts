import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import TUser from 'types/auth/TUser.ts';
import TCartProduct from 'types/TCartProduct.ts';

type TUserInfo = {
  isAuth: boolean;
  userInfo?: TUser;
  cart: TCartProduct[];
};
const initialState: TUserInfo = {
  isAuth: false,
  cart: [],
};
const profileSlice = createSlice({
  name: 'profile',
  initialState,
  reducers: {
    setIsAuth: (state, action: PayloadAction<boolean>) => {
      state.isAuth = action.payload;
    },
    setUserData: (
      state,
      action: PayloadAction<{ data: TUser } | undefined>,
    ) => {
      state.userInfo = action.payload ? action.payload.data : action.payload;
    },
    setCart: (state, action: PayloadAction<TCartProduct[]>) => {
      state.cart = action.payload;
    },
  },
});

export default profileSlice.reducer;

export const { setIsAuth, setUserData, setCart } = profileSlice.actions;
