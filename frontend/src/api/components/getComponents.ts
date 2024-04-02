import { AxiosResponse } from 'axios';
import axios from 'api/axios.ts';
import config from '../../../config.ts';
import componentTypes from 'enums/componentTypes.ts';
import TOneOfComponents from 'types/components/TOneOfComponents.ts';

async function getComponents(
  componentType: keyof typeof componentTypes,
  limit: number,
  offset: number,
): Promise<AxiosResponse<{ data: TOneOfComponents<string>[] }>> {
  return await axios.get(
    `${config.apiUrl}/${componentType}/getAll?limit=${limit}&offset=${offset}`,
  );
}

export default getComponents;
