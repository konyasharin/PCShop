import axios from 'api/axios.ts';
import config from '../../config.ts';
import TAssembly from 'types/TAssembly.ts';
import { AxiosResponse } from 'axios';

async function createAssembly(
  data: Omit<TAssembly<File>, 'id' | 'likes' | 'creationTime'>,
): Promise<AxiosResponse<TAssembly<string>>> {
  return await axios.post(`${config.apiUrl}/assembly/create`, data, {
    headers: { 'Content-Type': 'multipart/form-data' },
  });
}

export default createAssembly;
