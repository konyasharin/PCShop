import { AxiosResponse } from 'axios';
import axios from 'api/axios.ts';
import config from '../../../config.ts';

async function getComponents<T>(
  url: string,
): Promise<AxiosResponse<{ data: T }>> {
  return await axios.get(config.apiUrl + url);
}

export default getComponents;
