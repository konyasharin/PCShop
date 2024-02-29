import axios from '../../../axios.ts';
import TComputerCase from 'types/components/TComputerCase.ts';

export async function createComputerCase(data: Omit<TComputerCase, 'id'>) {
  const response = await axios.post<TComputerCase>(
    'ComputerCase/createComputerCase',
    data,
    { headers: { 'Content-Type': 'multipart/form-data' } },
  );
  console.log(response.data.height);
}

axios.post();
