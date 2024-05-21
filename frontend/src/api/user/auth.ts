import TRegistrationData from 'types/auth/TRegistrationData.ts';
import TLoginData from 'types/auth/TLoginData.ts';
import axiosModify from 'api/axios.ts';
import { Dispatch } from '@reduxjs/toolkit';
import { setIsLoading } from 'store/slices/loadingSlice.ts';
import axios, { AxiosError, AxiosResponse } from 'axios';
import { setIsAuth, setUserData } from 'store/slices/profileSlice.ts';
import getUser from 'api/user/getUser.ts';

/**
 * Производит аутентификацию пользователя
 * @param data
 * @param url
 * @param dispatch
 */
async function auth<T extends TRegistrationData | TLoginData>(
  data: T,
  url: string,
  dispatch: Dispatch,
): Promise<string | null> {
  dispatch(setIsLoading(true));
  try {
    const response: AxiosResponse<{ data: string } | null> =
      await axiosModify.post(url, data);
    if (response.data != null) {
      localStorage.setItem('token', response.data.data);
    }
    getUser().then(response => {
      dispatch(setUserData(response.data));
      dispatch(setIsAuth(true));
    });
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
