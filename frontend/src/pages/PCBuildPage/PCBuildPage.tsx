import Container from 'components/Container/Container.tsx';
import Scale from './Scale/Scale.tsx';
import { useEffect, useState } from 'react';
import BuildsError from './BuildsErrors/BuildsErrors.tsx';
import PowerOfBuild from './PowerOfBuild/PowerOfBuild.tsx';
import ChooseComponents from './ChooseComponents/ChooseComponents.tsx';
import Input from 'components/Input/Input.tsx';

function PCBuildPage() {
  const [scalePercents] = useState(40);
  const [buildName, setBuildName] = useState('');
  const [power, setPower] = useState<number>(0);
  useEffect(() => {
    setPower(5);
  }, []);
  return (
    <Container>
      <Input
        value={buildName}
        placeholder={'Название сборки'}
        onChange={(newBuildName: string) => setBuildName(newBuildName)}
      />
      <Scale percents={scalePercents} />
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
    </Container>
  );
}

export default PCBuildPage;
