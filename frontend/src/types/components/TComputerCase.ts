import TComponent from 'types/components/TComponent.ts';

type TComputerCase = TComponent & {
  material: string;
  width: number;
  height: number;
  depth: number;
};

export default TComputerCase;
