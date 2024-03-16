import { AxiosResponse } from 'axios';
import axios from 'api/axios.ts';
import config from '../../../config.ts';

async function getComponents<T>(
  url: string,
  limit: number,
  offset: number,
): Promise<AxiosResponse<{ data: T }>> {
  return await axios.get(
    config.apiUrl + url + `?limit=${limit}&offset=${offset}`,
  );
}

export default getComponents;
