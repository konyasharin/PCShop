import Container from 'components/Container/Container.tsx';
import LastBuild from './LastBuild/LastBuild.tsx';
import styles from './LastBuilds.module.css';
import arrowLeft from 'assets/arrow-left.png';
import React, { ReactNode } from 'react';
import TBuildPreview from 'types/TBuildPreview.ts';
import getLastBuilds from 'api/lastBuilds.ts';
import { useDispatch } from 'react-redux';
import { setIsLoading } from 'store/slices/loadingSlice.ts';

type LastBuildsProps = {
  lastBuilds: TBuildPreview[];
  setLastBuilds: React.Dispatch<React.SetStateAction<TBuildPreview[]>>;
};

const LastBuilds: React.FC<LastBuildsProps> = props => {
  const lastBuildsBlocks: ReactNode[] = props.lastBuilds.map(block => {
    return (
      <LastBuild
        name={block.name}
        img={block.img}
        description={block.description}
      />
    );
  });
  const dispatch = useDispatch();

  return (
    <section>
      <Container>
        <div className={styles.title}>
          <h2>Последние сборки</h2>
          <div className={styles.arrows}>
            <button
              onClick={() => {
                dispatch(setIsLoading(true));
                getLastBuilds().then(data => {
                  props.setLastBuilds(data);
                  dispatch(setIsLoading(false));
                });
              }}
            >
              <img src={arrowLeft} alt="arrow" />
            </button>
            <button
              onClick={() => {
                dispatch(setIsLoading(true));
                getLastBuilds().then(data => {
                  props.setLastBuilds(data);
                  dispatch(setIsLoading(false));
                });
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
