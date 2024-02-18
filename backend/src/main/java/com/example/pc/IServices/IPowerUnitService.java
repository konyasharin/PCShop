package com.example.pc.IServices;

import com.example.pc.Models.PowerUnitModel;

import java.util.List;

public interface IPowerUnitService {

    List<PowerUnitModel> getPowerUnits();
    PowerUnitModel getPowerUnitById(Long id);
    PowerUnitModel addPowerUnit(PowerUnitModel powerUnitModel);
    void deletePowerUnitById(Long id);
}
