import axios from 'api/axios.ts';
import config from '../../../config.ts';
import { AxiosResponse } from 'axios';

async function getComponent<T>(
  url: string,
  id: number,
): Promise<AxiosResponse<{ data: T }>> {
  return await axios.get(config.apiUrl + url + `/${id}`);
}

export default getComponent;
