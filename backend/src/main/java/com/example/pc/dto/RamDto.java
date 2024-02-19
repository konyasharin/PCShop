package com.example.pc.dto;

import lombok.Data;
import org.springframework.web.multipart.MultipartFile;

@Data
public class RamDto {

    private String brand;

    private String model;

    private Integer price;

    private String description;

    private String country;

    private Integer frequency;

    private Integer timings;

    private Integer capacity_gb;

    private MultipartFile image;
}
