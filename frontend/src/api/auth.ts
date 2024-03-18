import TRegistrationData from 'types/auth/TRegistrationData.ts';
import TLoginData from 'types/auth/TLoginData.ts';
import axiosModify from 'api/axios.ts';
import { Dispatch } from '@reduxjs/toolkit';
import { setIsLoading } from 'store/slices/loadingSlice.ts';
import axios, { AxiosError, AxiosResponse } from 'axios';
import TUser from 'types/auth/TUser.ts';
import { setIsAuth, setUserData } from 'store/slices/profileSlice.ts';

async function auth<T extends TRegistrationData | TLoginData>(
  data: T,
  url: string,
  dispatch: Dispatch,
): Promise<string | null> {
  dispatch(setIsLoading(true));
  try {
    const response: AxiosResponse<{ data: TUser }> = await axiosModify.post(
      url,
      data,
    );
    dispatch(setUserData(response.data));
    dispatch(setIsAuth(true));
  } catch (error) {
    if (axios.isAxiosError(error) && error.response) {
      const axiosError = error as AxiosError<{ error: string }>;
      dispatch(setIsLoading(false));
      return axiosError.response!.data.error;
    }
  }
  dispatch(setIsLoading(false));
  return null;
}

export default auth;
