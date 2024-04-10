import TAssembly from 'types/TAssembly.ts';
import axios from 'api/axios.ts';
import config from '../../../config.ts';
import { AxiosResponse } from 'axios';

/**
 * Получает популярные сборки (3 сборки которые чаще всего добавляли в избранное)
 */
async function getPopularBuilds(): Promise<
  AxiosResponse<{ assemblies: TAssembly<string>[] }>
> {
  return await axios.get(`${config.apiUrl}/assembly/getPopular`);
}

export default getPopularBuilds;
