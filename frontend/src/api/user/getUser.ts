import axios from 'api/axios.ts';
import config from '../../../config.ts';
import { AxiosResponse } from 'axios';
import TUser from 'types/auth/TUser.ts';

function getUser(): Promise<AxiosResponse<{ data: TUser }>> {
  return axios.get(
    `${config.apiUrl}/User/getUser?token=${localStorage.getItem('token')}`,
  );
}

export default getUser;
