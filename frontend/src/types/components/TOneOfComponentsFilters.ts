import TMotherBoardFilters from 'types/components/TMotherBoardFilters.ts';
import TVideoCardFilters from 'types/components/TVideoCardFilters.ts';
import TProcessorFilters from 'types/components/TProcessorFilters.ts';

type TOneOfComponentsFilters =
  | TMotherBoardFilters
  | TVideoCardFilters
  | TProcessorFilters;

export default TOneOfComponentsFilters;
