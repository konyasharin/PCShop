import Container from 'components/Container/Container.tsx';
import Scale from './Scale/Scale.tsx';
import { useEffect, useState } from 'react';
import BuildsError from './BuildsError/BuildsError.tsx';
import PowerOfBuild from './PowerOfBuild/PowerOfBuild.tsx';
import ChooseComponents from './ChooseComponents/ChooseComponents.tsx';
import Input from 'components/Input/Input.tsx';
import styles from './PCBuildPage.module.css';
import Btn from 'components/Btn/Btn.tsx';

function PCBuildPage() {
  const [scalePercents] = useState(40);
  const [buildName, setBuildName] = useState('');
  const [power, setPower] = useState<number>(0);
  useEffect(() => {
    setPower(5);
  }, []);
  return (
    <div className={styles.body}>
      <Container>
        <h2 className={styles.build}>Ваша сборка</h2>
        <Input
          className={styles.input}
          value={buildName}
          placeholder={'Название сборки'}
          onChange={(newBuildName: string) => setBuildName(newBuildName)}
        />
        <Scale className={styles.scale} percents={scalePercents} />
        <BuildsError
          type={'Warning'}
          title={'Процессор'}
          description={'Выберите процессор из списка'}
        />
        <BuildsError
          type={'Error'}
          title={'Процессор'}
          description={'Выберите процессор из списка'}
        />
        <ChooseComponents />
        <PowerOfBuild power={power} price={'100$'} />
        <Btn className={styles.button}>Cоздать пк</Btn>
      </Container>
    </div>
  );
}

export default PCBuildPage;
