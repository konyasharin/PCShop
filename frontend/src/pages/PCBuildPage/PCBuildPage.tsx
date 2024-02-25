import Container from 'components/Container/Container.tsx';
import Scale from './Scale/Scale.tsx';
import { useState } from 'react';
import BuildsError from './BuildsError/BuildsError.tsx';
import ChooseComponents from './ChooseComponents/ChooseComponents.tsx';
import Input from 'components/Input/Input.tsx';

function PCBuildPage() {
  const [scalePercents] = useState(40);
  const [buildName, setBuildName] = useState('');
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
    </Container>
  );
}

export default PCBuildPage;
