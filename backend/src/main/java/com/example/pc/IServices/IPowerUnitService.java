package com.example.pc.IServices;

import com.example.pc.Models.PowerUnitModel;
import com.example.pc.dto.PowerUnitDto;

import java.util.List;

public interface IPowerUnitService {

    List<PowerUnitModel> getPowerUnits();
    PowerUnitModel getPowerUnitById(Long id);
    PowerUnitModel updatePowerUnit(PowerUnitModel powerUnitModel);
    void deletePowerUnitById(Long id);
    PowerUnitModel createPowerUnit(PowerUnitDto powerUnitDto);
}
