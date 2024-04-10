import axios from 'api/axios.ts';
import config from '../../../config.ts';
import { AxiosResponse } from 'axios';
import TAssembly from 'types/TAssembly.ts';

/**
 * Получает сборки
 * @param limit ограничение по количеству сборок которые нужно получить
 * @param offset количество сборок которые нужно пропустить
 */
function getBuilds(
  limit: number,
  offset: number,
): Promise<AxiosResponse<{ assemblies: TAssembly<string>[] }>> {
  return axios.get(
    `${config.apiUrl}/assembly/getAll?limit=${limit}&offset=${offset}`,
  );
}

export default getBuilds;
