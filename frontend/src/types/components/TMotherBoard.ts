import TMotherBoardFilters from 'types/components/TMotherBoardFilters.ts';
import TComponent from 'types/components/TComponent.ts';

type TMotherBoard<T extends string | File> = TMotherBoardFilters &
  TComponent<T>;

export default TMotherBoard;
