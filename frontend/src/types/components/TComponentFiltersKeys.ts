import TVideoCardFilters from 'types/components/TVideoCardFilters.ts';
import TProcessorFilters from 'types/components/TProcessorFilters.ts';
import { TComponentFilterKeys } from 'store/slices/filtersSlice.ts';

type TComponentFiltersKeys =
  | TComponentFilterKeys<TVideoCardFilters>
  | TComponentFilterKeys<TProcessorFilters>;

export default TComponentFiltersKeys;
