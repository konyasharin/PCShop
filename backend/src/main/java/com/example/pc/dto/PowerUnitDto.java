package com.example.pc.dto;

import lombok.Data;
import org.springframework.web.multipart.MultipartFile;

@Data
public class PowerUnitDto {

    private String brand;

    private String model;

    private Integer price;

    private String description;

    private String country;

    private String battery;

    private Integer voltage;

    private MultipartFile image;
}
