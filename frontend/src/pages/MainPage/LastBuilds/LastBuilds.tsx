import Container from 'components/Container/Container.tsx';
import LastBuild from './LastBuild/LastBuild.tsx';
import styles from './LastBuilds.module.css';
import arrowLeft from 'assets/arrow-left.png';
import React, { ReactNode } from 'react';
import TAssembly from 'types/TAssembly.ts';

type LastBuildsProps = {
  lastBuilds: TAssembly<string>[];
  offset: number;
  setOffset: (newOffset: number) => void;
};

const LastBuilds: React.FC<LastBuildsProps> = props => {
  const lastBuildsBlocks: ReactNode[] = props.lastBuilds.map(assembly => {
    return <LastBuild assembly={assembly} />;
  });

  return (
    <section>
      <Container>
        <div className={styles.title}>
          <h2>Последние сборки</h2>
          <div className={styles.arrows}>
            <button
              onClick={() => {
                props.setOffset(props.offset - 3);
              }}
            >
              <img src={arrowLeft} alt="arrow" />
            </button>
            <button
              onClick={() => {
                props.setOffset(props.offset + 3);
              }}
            >
              <img src={arrowLeft} alt="arrow" />
            </button>
          </div>
        </div>
        <div className={styles.blocks}>{...lastBuildsBlocks}</div>
      </Container>
    </section>
  );
};

export default LastBuilds;
