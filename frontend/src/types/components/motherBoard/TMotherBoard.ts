import TMotherBoardFilters from 'types/components/motherBoard/TMotherBoardFilters.ts';
import TComponent from 'types/components/TComponent.ts';

type TMotherBoard<T extends string | File> = TMotherBoardFilters &
  TComponent<T>;

export default TMotherBoard;
