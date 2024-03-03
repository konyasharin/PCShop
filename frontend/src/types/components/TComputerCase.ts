import TComponent from 'types/components/TComponent.ts';

type TComputerCase<T extends File | string> = TComponent<T> & {
  material: string;
  width: number;
  height: number;
  depth: number;
};

export default TComputerCase;
