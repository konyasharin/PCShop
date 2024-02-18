import { useSelector } from 'react-redux';
import { RootState } from '../store/store.ts';

function useSearch() {
  const isLoading = useSelector((state: RootState) => state.loading.isLoading);

  return { isLoading };
}

export default useSearch;
