package com.example.pc.IServices;

import com.example.pc.Models.SSDModel;

import java.util.List;

public interface ISSDService {

    List<SSDModel> getSSD();
    SSDModel getSSDById(Long id);
    SSDModel addSSD(SSDModel ssdModel);
    void deleteSSDById(Long id);
}
