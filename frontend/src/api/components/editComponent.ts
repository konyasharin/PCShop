import axios from 'api/axios.ts';
import config from '../../../config.ts';
import componentTypes from 'enums/componentTypes.ts';
import TOneOfComponents from 'types/components/TOneOfComponents.ts';
import { AxiosResponse } from 'axios';

function editComponent(
  componentType: keyof typeof componentTypes,
  id: string,
  data: Omit<TOneOfComponents<File>, 'productId'>,
  isUpdated: boolean,
): Promise<AxiosResponse<TOneOfComponents<string>>> {
  return axios.put(
    `${config.apiUrl}/${componentType}/update/${id}?isUpdated=${isUpdated}`,
    data,
    {
      headers: { 'Content-Type': 'multipart/form-data' },
    },
  );
}

export default editComponent;
