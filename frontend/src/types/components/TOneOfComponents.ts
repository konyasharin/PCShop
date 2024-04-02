import TVideoCard from 'types/components/videoCard/TVideoCard.ts';
import TProcessor from 'types/components/processor/TProcessor.ts';
import TMotherBoard from 'types/components/motherBoard/TMotherBoard.ts';
import TRAM from 'types/components/RAM/TRAM.ts';
import TComputerCase from 'types/components/computerCase/TComputerCase.ts';
import TCooler from 'types/components/cooler/TCooler.ts';
import TPowerUnit from 'types/components/powerUnit/TPowerUnit.ts';
import TSSD from 'types/components/SSD/TSSD.ts';

type TOneOfComponents<T extends File | string> =
  | TVideoCard<T>
  | TProcessor<T>
  | TMotherBoard<T>
  | TRAM<T>
  | TComputerCase<T>
  | TCooler<T>
  | TPowerUnit<T>
  | TSSD<T>;

export default TOneOfComponents;
