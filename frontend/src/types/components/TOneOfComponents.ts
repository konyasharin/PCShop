import TVideoCard from 'types/components/TVideoCard.ts';
import TProcessor from 'types/components/TProcessor.ts';
import TMotherBoard from 'types/components/TMotherBoard.ts';

type TOneOfComponents<T extends File | string> =
  | TVideoCard<T>
  | TProcessor<T>
  | TMotherBoard<T>;

export default TOneOfComponents;
