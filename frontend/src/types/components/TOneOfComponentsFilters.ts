import TMotherBoardFilters from 'types/components/motherBoard/TMotherBoardFilters.ts';
import TVideoCardFilters from 'types/components/videoCard/TVideoCardFilters.ts';
import TProcessorFilters from 'types/components/processor/TProcessorFilters.ts';
import TRAMFilters from 'types/components/RAM/TRAMFilters.ts';
import TComputerCaseFilters from 'types/components/computerCase/TComputerCaseFilters.ts';
import TCoolerFilters from 'types/components/cooler/TCoolerFilters.ts';
import TPowerUnitFilters from 'types/components/powerUnit/TPowerUnitFilters.ts';
import TSSDFilters from 'types/components/SSD/TSSDFilters.ts';

type TOneOfComponentsFilters =
  | TMotherBoardFilters
  | TVideoCardFilters
  | TProcessorFilters
  | TRAMFilters
  | TComputerCaseFilters
  | TCoolerFilters
  | TPowerUnitFilters
  | TSSDFilters;

export default TOneOfComponentsFilters;
