package com.example.pc.IServices;

import com.example.pc.Models.ComputerCaseModel;

import java.util.List;

public interface IComputerCaseService {

    List<ComputerCaseModel> getAllComputerCases();
    ComputerCaseModel getComputerCaseById(Long id);
    ComputerCaseModel addComputerCase(ComputerCaseModel computerCaseModel);
    void deleteComputerCaseById(Long id);
}
