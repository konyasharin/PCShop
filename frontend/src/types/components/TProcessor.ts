import TComponent from 'types/components/TComponent.ts';

type TProcessor<T extends string | File> = TComponent<T> & {
  cores: number;
  clock_frequency: number;
  turbo_frequency: number;
  heat_dissipation: number;
};

export default TProcessor;
