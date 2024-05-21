import TUser from 'types/auth/TUser.ts';
import { AxiosResponse } from 'axios';
import axios from 'api/axios.ts';
import config from '../../../config.ts';

function editUser(newUserData: TUser): Promise<AxiosResponse<{ data: TUser }>> {
  return axios.put(`${config.apiUrl}/User/edit/${newUserData.id}`, newUserData);
}

export default editUser;
