import axios from '../../../axios.ts';
import TComputerCase from 'types/components/TComputerCase.ts';
import TCreateComponentResponse from 'types/TCreateComponentResponse.ts';

export async function createComputerCase(
  data: Omit<TComputerCase, 'id'>,
): Promise<TCreateComponentResponse<TComputerCase>> {
  return await axios.post<TComputerCase>(
    'ComputerCase/createComputerCase',
    data,
    {
      headers: { 'Content-Type': 'multipart/form-data' },
    },
  );
}

axios.post();
